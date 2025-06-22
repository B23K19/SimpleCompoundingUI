import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';  

interface compoundInterestResult {

  totalYears: number;
  totalContributions: number;
  totalInterestAfterTax: number;
  totalInterestBeforeTax: number;
  finalBalance: number;
  inflationAdjustedBalance: number;

}

@Component({
  selector: 'app-landing',
  standalone: false,
  templateUrl: './landing.html',
  styleUrl: './landing.css'
})
export class Landing {
  simpleCompoundForm: FormGroup;
  result: compoundInterestResult | null = null;
  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.simpleCompoundForm = this.fb.group({
      initialInvestment: ['', Validators.required],
      rate: ['', Validators.required],
      interestType: [],
      makeContributions: [false],
      contribution: [],
      contributionFrequency: [1],
      years: [],
      compoundFrequency: [1],
      inflationRate: [],
      taxRate: []
    });
  }
  onSubmit() {
    if (this.simpleCompoundForm.valid) {
      const formData = this.simpleCompoundForm.value;

      const body = {
        initialInvestment: formData.initialInvestment,
        rate: formData.rate,
        makeContributions: formData.makeContributions,
        contribution: formData.contribution || 0,
        contributionFrequency: formData.contributionFrequency,
        years: formData.years,
        compoundFrequency: formData.compoundFrequency,
        inflationRate: formData.inflationRate,
        taxRate: formData.taxRate
      };

      this.http.post<compoundInterestResult>('http://localhost:5070/api/Interest/calculate', body)
        .subscribe({
          next: (res) => {
            this.result = res;
          },
          error: (err) => {
            console.error('API Error:', err);
          }
        });
    } else {
      console.log('Form is invalid');
    }
  }


}
