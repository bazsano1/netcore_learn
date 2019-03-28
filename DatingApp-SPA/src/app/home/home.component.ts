import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
/** home component*/
export class HomeComponent implements OnInit {
  ngOnInit(): void {
    this.getValues();
  }
  values: any;
  /** value ctor */
  constructor(private http: HttpClient) {
  }

  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(_ => {
      this.values = _;
    }, error => {
      console.log(error);
    })
  }

  registerMode = false;

  registerToggle() {
    this.registerMode = !this.registerMode;
  }
}
