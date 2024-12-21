import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DescriptionService {
  getDescription(value: number): string {
    if (isNaN(value) || value > 10 || value < 0) {
      return 'ERROR';
    } else if (value <= 5.9) {
      return 'BJ';
    } else if (value > 5.9 && value <= 7.9) {
      return 'BS';
    } else if (value >7.9 && value <= 8.9) {
      return 'A';
    } else if (value > 8.9 && value <= 10) {
      return 'S';
    }
    return 'ERROR';
  }
}
