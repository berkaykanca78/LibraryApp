import { Injectable } from '@angular/core';

@Injectable()
export class GlobalVariables {
  phoneRegex = /(5+[0-9]{2})([0-9]{3})([0-9]{2})([0-9]{2})$/;
  passwordRegex = /^(?=.{15,15}$)(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_.])/;
  dateFormat = 'DD.MM.YYYY';
  dateTimeFormat = 'DD.MM.YYYY HH:mm:ss';
  timeFormat = 'HH:mm:ss';
}
