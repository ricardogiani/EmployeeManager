import { Component, EventEmitter, inject, Input, input, OnInit, Output, signal } from '@angular/core';
import { Employee } from '../../interfaces/employee';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { JobLevelEnum } from '../../enums/job-level-enum';
import { EmployeeService } from '../../services/employee/employee.service';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-employee-detail',
  standalone: true,
  imports:[
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

  loading: boolean = false;

  route: ActivatedRoute = inject(ActivatedRoute);

  errorFromServer = signal<string[]>([]);

  @Input() allEmployees: Employee[] = [];

  employeeService: EmployeeService = inject(EmployeeService);

  editEmployeeId: number = 0;
  employeeForm!: FormGroup;
  isEditMode: boolean = false;

  JobLevelEnum = JobLevelEnum;
  jobLevels = Object.values(JobLevelEnum).filter(value => typeof value === 'number' && value !== JobLevelEnum.None);

  constructor(private fb: FormBuilder, private router: Router) { 
    this.editEmployeeId = Number(this.route.snapshot.params['id']);
  }

  ngOnInit() {

    this.initForm();
    this.isEditMode = (this.editEmployeeId > 0);

    this.getEmployees(JobLevelEnum.Intern);

    if (this.isEditMode) {
      this.getEmployeById(this.editEmployeeId);      
    } else {
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


  getEmployeById(employeeId: number) : void {

    this.employeeService.getById(employeeId).subscribe({
      
      next: (value: Employee) => {        
        this.populateForm(value);        
      },
      error: (err: any) => { 
        console.error(`Erro ao buscar ${employeeId}:`, err);
      },      
      complete: () => {
        console.log(`Busca por ${employeeId} concluída.`);        
      }
    });        
  }

  getEmployees(jobLevel: JobLevelEnum) : void {
    this.employeeService.getByFilter({ jobLevel: jobLevel, JobLevelUp: true }).subscribe({      
      next: (value: Employee[]) => {

        this.allEmployees = value;

        console.error(`Funcionários carregados`);
      },
      error: (err: any) => { 
        console.error(`Erro ao buscar Managers ${jobLevel}:`, err);
      },      
      complete: () => {
        console.log(`Busca por Managers acima de ${jobLevel} concluída.`);        
      }
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

  update(employeeId: number, employee: Employee) : void {    
    this.clearMessages();
    this.employeeService.update(employeeId, employee).subscribe({
        next: (updatedEmployee) => {
          console.log('Funcionário atualizado com sucesso!', updatedEmployee);
          this.loading = false;
          this.router.navigate(['/employee-list']);
        },
        error: (err) => {
          this.errorFromServer.set((err as HttpErrorResponse).error);          
          this.loading = false;
          console.error(err);
        }
      });
  }

  create(employee: Employee) : void {
    this.clearMessages();
    this.employeeService.create(employee).subscribe({
        next: (createdEmployee) => {
          console.log('Funcionário cadastrado com sucesso!', createdEmployee);
          this.loading = false;
          this.router.navigate(['/employee-list']);
        },
        error: (err) => {          
          this.errorFromServer.set((err as HttpErrorResponse).error);          
          this.loading = false;
          console.error(err);
        }
      });
  }

  clearMessages()
  {
    this.errorFromServer.set([]);
  }

  onSubmit(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      return;
    }
    const formData = this.employeeForm.getRawValue();

    if (this.isEditMode) {
      delete formData.password;

      this.update(this.editEmployeeId, formData as Employee);
    }
    else
      this.create(formData as Employee);
  }

  onCancel(): void {
    this.router.navigate(['/employee-list']);
  }

}
