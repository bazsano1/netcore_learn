import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-value',
    templateUrl: './value.component.html',
    styleUrls: ['./value.component.css']
})
/** value component*/
export class ValueComponent implements OnInit {
    ngOnInit(): void {
        this.getValues();
    }
  values: any;

    /** value ctor */
    constructor(private http:HttpClient) {
  }
  


  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(_ => {
      this.values = _;
    }, error => 
    {
      console.log(error);
    })
  }

}
