import { Component, input, OnInit } from '@angular/core';
import { Employee } from '../../interfaces/employee';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

  constructor() { }

  //employee = input.required<Employee>();

  ngOnInit() {
  }

}
