import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';

import { UsuarioComponent } from './usuario/usuario.component';
import { CicloComponent } from './ciclo/ciclo.component';
import { ProfesorComponent } from './profesor/profesor.component';
import { EstudianteComponent } from './estudiante/estudiante.component';
import { MateriaComponent } from './materia/materia.component';
import { PersonaResponsableComponent } from './persona-responsable/persona-responsable.component';
import { MatriculaComponent } from './matricula/matricula.component';
import { AsistenciaComponent } from './asistencia/asistencia.component';
import { CalificacionComponent } from './calificacion/calificacion.component';
import { CheckGradesComponent } from './check-grades/check-grades.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },  // Ruta inicial
  { path: 'usuarios', component: UsuarioComponent },
  { path: 'ciclos', component: CicloComponent },
  { path: 'profesores', component: ProfesorComponent },
  { path: 'estudiantes', component: EstudianteComponent },
  { path: 'materias', component: MateriaComponent },
  { path: 'personas-responsables', component: PersonaResponsableComponent },
  { path: 'matriculas', component: MatriculaComponent },
  { path: 'asistencias', component: AsistenciaComponent },
  { path: 'calificaciones', component: CalificacionComponent },
  { path: 'login', component: LoginComponent },
  { path: 'check-grades', component: CheckGradesComponent },
];