import { Component, EventEmitter, inject, Input, input, OnInit, Output } from '@angular/core';
import { Employee } from '../../interfaces/employee';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { JobLevelEnum } from '../../enums/job-level-enum';

@Component({
  selector: 'app-employee-detail',
  standalone: true,
  imports:[
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

  route: ActivatedRoute = inject(ActivatedRoute);

  @Input() employee: Employee | null = null;
  @Output() formSubmit = new EventEmitter<Employee>();
  @Output() cancelForm = new EventEmitter<void>();

  employeeId: number = 0;
  employeeForm!: FormGroup;
  isEditMode: boolean = false;

  JobLevelEnum = JobLevelEnum;
  jobLevels = Object.values(JobLevelEnum).filter(value => typeof value === 'number' && value !== JobLevelEnum.None);

  constructor(private fb: FormBuilder) { 
    this.employeeId = Number(this.route.snapshot.params['id']);
  }

  //employee = input.required<Employee>();

  ngOnInit() {
    this.initForm();

    if (this.employee) {
      this.isEditMode = true;
      this.populateForm(this.employee);
    } else {
      this.isEditMode = false;
      this.employeeForm.get('password')?.setValidators(Validators.required);
      this.employeeForm.get('password')?.updateValueAndValidity();
    }
  }

  initForm(): void {
    this.employeeForm = this.fb.group({
      id: [{ value: null, disabled: true }], // ID: disabled para readonly
      active: [true, Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      documentNumber: ['', Validators.required],
      password: [''],
      birthDate: ['', Validators.required],
      jobLevel: [JobLevelEnum.None, Validators.required],
      phoneNumber: ['', Validators.required],
      managerId: [null]
    });
  }

  populateForm(employee: Employee): void {
    this.employeeForm.get('password')?.clearValidators();
    this.employeeForm.get('password')?.updateValueAndValidity();

    this.employeeForm.patchValue({
      id: employee.id,
      active: employee.active,
      firstName: employee.firstName,
      lastName: employee.lastName,
      email: employee.email,
      documentNumber: employee.documentNumber,
      birthDate: employee.birthDate ? new Date(employee.birthDate).toISOString().substring(0, 10) : '',
      jobLevel: employee.jobLevel,
      phoneNumber: employee.phoneNumber,
      managerId: employee.managerId
    });
  }

  onSubmit(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      return;
    }

    const formData = this.employeeForm.getRawValue();

    if (this.isEditMode && !formData.password) {
      delete formData.password;
    }

    this.formSubmit.emit(formData as Employee);
  }

  onCancel(): void {
    this.cancelForm.emit();
  }

}
