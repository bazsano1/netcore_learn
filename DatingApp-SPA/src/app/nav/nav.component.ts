import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})
/** nav component*/
export class NavComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService) {

  }
  ngOnInit() {

  } 


  login() {
    this.authService.login(this.model).subscribe(next => {
        console.log("logged in successfully");
      },
      error => {
        console.log("Failed to login");
      });
  }


  register() {
    this.authService.register(this.model).subscribe();
  }
}
