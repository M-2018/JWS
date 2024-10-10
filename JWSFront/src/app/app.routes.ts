import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { UserRegistrationComponent } from './user-registration/user-registration.component';
import { CycleSubjectComponent } from './cycle-subject/cycle-subject.component';
import { TeacherComponent } from './teacher/teacher.component';
import { StudentComponent } from './student/student.component';
import { EnrollmentComponent } from './enrollment/enrollment.component';
import { AttendanceComponent } from './attendance/attendance.component';
import { GradesComponent } from './grades/grades.component';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' }, // Redirecciona a Home cuando la ruta está vacía
  { path: 'home', component: HomeComponent },
  { path: 'user-registration', component: UserRegistrationComponent },
  { path: 'cycle-subject', component: CycleSubjectComponent },
  { path: 'teacher', component: TeacherComponent },
  { path: 'student', component: StudentComponent },
  { path: 'enrollment', component: EnrollmentComponent },
  { path: 'attendance', component: AttendanceComponent },
  { path: 'grades', component: GradesComponent },
];