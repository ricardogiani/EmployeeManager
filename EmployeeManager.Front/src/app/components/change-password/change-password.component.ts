import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports:[CommonModule, 
    ReactiveFormsModule, 
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,   
    MatNativeDateModule
  ],
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  errorFromServer = signal<string>('');
  passwordForm!: FormGroup;

  constructor(private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.initForm();

    this.passwordForm.get('password')?.setValidators(Validators.required);
    this.passwordForm.get('password')?.updateValueAndValidity();

    this.passwordForm.get('newpassword')?.setValidators(Validators.required);
    this.passwordForm.get('newpassword')?.updateValueAndValidity();

    this.passwordForm.get('newpasswordConfirmation')?.setValidators(Validators.required);
    this.passwordForm.get('newpasswordConfirmation')?.updateValueAndValidity();

  }

  initForm(): void {
    this.passwordForm = this.fb.group({
      id: [{ value: null, disabled: true }], // ID: disabled para readonly
      password: ['', [Validators.required, Validators.min(8), Validators.maxLength(16)]],
      newPassword: ['', [Validators.required, Validators.min(8), Validators.maxLength(16)]],
      newPasswordConfirmation: ['', [Validators.required, Validators.min(8), Validators.maxLength(16)]]
    });
  }

  onSubmit(): void {
      if (this.passwordForm.invalid) {
        this.passwordForm.markAllAsTouched();
        return;
      }
      const formData = this.passwordForm.getRawValue();

      // TODO call API
    }
  
    onCancel(): void {
      this.router.navigate(['/employee-list']);
    }

}
