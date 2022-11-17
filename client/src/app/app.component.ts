import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Exam Project';
  products: any[];


  constructor(private http : HttpClient) {}

  ngOnInit(): void {
    this.http.get("https://localhost:5001/api/Products").subscribe((response: any) => {
      this.products = response;
     // console.log(response);
    } , error => {
      console.log(error)
    })
  }
}
