import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CalculationServiceService } from './calculation-service.service';
import { CalculationResultDto } from '../dto/calculation-result-dto';

describe('CalculationServiceService', () => {
  let service: CalculationServiceService;
  let httpMock: HttpTestingController;
  const apiUrl = '/api/Simulation/getCalculation';

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CalculationServiceService],
    });

    service = TestBed.inject(CalculationServiceService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call the API with correct query params and return the expected result', () => {
    const mockResponse: CalculationResultDto = {
      bruteAmount: 110.16,
      liquidAmount: 102.03,
      tributeAmount: 8.13,
      tributePercent: 20.0,
    };

    service.getCalculationResult(100, 10).subscribe((result) => {
      expect(result).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(
      `${apiUrl}?initialAmount=100&mounthQuantity=10`
    );
    expect(req.request.method).toBe('GET');

    req.flush(mockResponse);
  });

  it('should handle multiple calls with different query params correctly', () => {
    const responseOne: CalculationResultDto = {
      bruteAmount: 120.00,
      liquidAmount: 110.00,
      tributeAmount: 10.00,
      tributePercent: 18.0,
    };
    const responseTwo: CalculationResultDto = {
      bruteAmount: 150.00,
      liquidAmount: 135.00,
      tributeAmount: 15.00,
      tributePercent: 20.0,
    };

    service.getCalculationResult(150, 12).subscribe((result) => {
      expect(result).toEqual(responseOne);
    });

    service.getCalculationResult(200, 18).subscribe((result) => {
      expect(result).toEqual(responseTwo);
    });

    const req1 = httpMock.expectOne(`${apiUrl}?initialAmount=150&mounthQuantity=12`);
    const req2 = httpMock.expectOne(`${apiUrl}?initialAmount=200&mounthQuantity=18`);

    req1.flush(responseOne);
    req2.flush(responseTwo);

    expect(req1.request.method).toBe('GET');
    expect(req2.request.method).toBe('GET');
  });

  it('should handle HTTP errors gracefully', () => {
    service.getCalculationResult(100, 10).subscribe(
      () => fail('Expected error, but got success response'),
      (error) => {
        expect(error.status).toBe(500);
      }
    );

    const req = httpMock.expectOne(`${apiUrl}?initialAmount=100&mounthQuantity=10`);
    req.flush('Internal Server Error', { status: 500, statusText: 'Server Error' });
  });
});
