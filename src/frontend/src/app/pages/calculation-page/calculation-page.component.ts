import { Component } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, Validators } from '@angular/forms';
import { CalculationResultDto } from 'src/app/dto/calculation-result-dto';
import { CalculationServiceService } from 'src/app/services/calculation-service.service';

@Component({
  selector: 'app-calculation-page',
  templateUrl: './calculation-page.component.html',
  styleUrls: ['./calculation-page.component.css']
})
export class CalculationPageComponent {
  calcForm: UntypedFormGroup;
  simulationResult?: CalculationResultDto;

  constructor(private fb: UntypedFormBuilder, private _service: CalculationServiceService) {
    this.calcForm = this.fb.group({
      initialInvestment: [null, [Validators.required, Validators.min(0)]],
      months: [null, [Validators.required, Validators.min(1)]],
    });
  }

  simulateCalculation(): void {
    if (this.calcForm.valid) {
      this._service.getCalculationResult(this.calcForm.value.initialInvestment, this.calcForm.value.months).subscribe(result => {
        this.simulationResult = { 
          bruteAmount: result.bruteAmount ?? 0, 
          liquidAmount: result.liquidAmount ?? 0,
          tributeAmount: result.tributeAmount ?? 0,
          tributePercent: result.tributePercent ?? 0
        }
      })
    }
  }
}
