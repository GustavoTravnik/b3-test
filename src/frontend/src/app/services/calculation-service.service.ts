import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CalculationResultDto } from '../dto/calculation-result-dto';

@Injectable({
  providedIn: 'root'
})
export class CalculationServiceService {

  private apiUrl = '/api/Simulation/getCalculation'; 

  constructor(private http: HttpClient) { }

  public getCalculationResult(initialInvestment: number, months:number): Observable<CalculationResultDto> {
    const params = new HttpParams()
      .set('initialAmount', initialInvestment.toString())
      .set('mounthQuantity', months.toString());

    return this.http.get<CalculationResultDto>(this.apiUrl, { params });
  }
}