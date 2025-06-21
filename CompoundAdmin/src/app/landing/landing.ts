import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';


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
  constructor(private fb: FormBuilder) {
    this.simpleCompoundForm = this.fb.group({
      principal: ['', Validators.required],
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
    (this.simpleCompoundForm.valid) 
      console.log('Form Submitted!', this.simpleCompoundForm.value);
     
    
  }

}
