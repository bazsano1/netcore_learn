import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
/** register component*/
export class RegisterComponent {
  model: any = {};
  @Input() valuesFromHome: any;
  
  constructor() {

  }
  register() {
    console.log(this.model);
  }

  cancel() {
    console.log('cancelled');
  }

}
