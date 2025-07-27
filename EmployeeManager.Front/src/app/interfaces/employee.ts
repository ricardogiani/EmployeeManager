import { JobLevelEnum } from "../enums/job-level-enum";

export interface Employee {

  id?: number;
  active: boolean;
  firstName: string;
  lastName: string;
  email: string;
  documentNumber: string;
  password?: string; // Campo opcional se não for sempre necessário no front-end
  birthDate: Date;
  jobLevel: JobLevelEnum; // Você precisará definir o enum JobLevelEnum
  phoneNumber: string;
  managerId?: number;
  createdAt?: Date;
  updatedAt?: Date;

}
