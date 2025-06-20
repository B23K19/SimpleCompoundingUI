import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-landing',
  standalone: false,
  templateUrl: './landing.html',
  styleUrl: './landing.css'
})
export class Landing {
  simpleCompoundForm: FormGroup;
  constructor(private fb: FormBuilder) {
    this.simpleCompoundForm = this.fb.group({
      principal: [],
      rate: [],
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
      console.log('Form Submitted!', this.simpleCompoundForm.value);
      // Here you would typically send the form data to a server
    } else {
      console.log('Form is invalid');
    }
  }

}
