import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { CalculationPageComponent } from './calculation-page.component';
import { CalculationServiceService } from 'src/app/services/calculation-service.service';
import { of } from 'rxjs';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CalculationPageComponent', () => {
  let component: CalculationPageComponent;
  let fixture: ComponentFixture<CalculationPageComponent>;
  let mockService: jasmine.SpyObj<CalculationServiceService>;

  beforeEach(async () => {
    mockService = jasmine.createSpyObj('CalculationServiceService', ['getCalculationResult']);

    await TestBed.configureTestingModule({
      declarations: [CalculationPageComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule],
      providers: [
        { provide: CalculationServiceService, useValue: mockService },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CalculationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize the form with default values', () => {
    expect(component.calcForm.value).toEqual({ initialInvestment: null, months: null });
  });

  it('should validate form controls correctly', () => {
    const initialInvestmentControl = component.calcForm.get('initialInvestment');
    const monthsControl = component.calcForm.get('months');

    initialInvestmentControl?.setValue(-1);
    monthsControl?.setValue(0);

    expect(initialInvestmentControl?.valid).toBeFalse();
    expect(monthsControl?.valid).toBeFalse();

    initialInvestmentControl?.setValue(100);
    monthsControl?.setValue(10);

    expect(initialInvestmentControl?.valid).toBeTrue();
    expect(monthsControl?.valid).toBeTrue();
  });

  it('should call the service and set the simulation result', () => {
    const mockResult = {
      bruteAmount: 110.16,
      liquidAmount: 102.03,
      tributeAmount: 8.13,
      tributePercent: 20.0,
    };
    mockService.getCalculationResult.and.returnValue(of(mockResult));

    component.calcForm.setValue({ initialInvestment: 100, months: 10 });
    component.simulateCalculation();

    expect(mockService.getCalculationResult).toHaveBeenCalledWith(100, 10);
    expect(component.simulationResult).toEqual(mockResult);
  });

  it('should not call the service if the form is invalid', () => {
    component.calcForm.setValue({ initialInvestment: null, months: null });
    component.simulateCalculation();

    expect(mockService.getCalculationResult).not.toHaveBeenCalled();
  });
});
