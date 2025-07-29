import { JobLevelEnum } from "../enums/job-level-enum";

export interface EmployeeFilter {
  email?: string;
  phoneNumber?: string;
  jobLevel?: JobLevelEnum;
  JobLevelUp?: boolean; 
  active?: boolean;
}
