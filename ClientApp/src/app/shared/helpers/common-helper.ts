import { DatePipe } from "@angular/common";
import { Injectable } from "@angular/core";
import { DateHelper } from "./date-helper";

@Injectable({
  providedIn: 'root'
})
export class CommonHelper {
  constructor(public dateHelper: DateHelper, public datePipe: DatePipe) {
  }

  validation = {
    isEmailAddress: (str) => {
      const pattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
      return pattern.test(str);
    },
    isNotEmpty: (str) => {
      const pattern = /\S+/;
      return pattern.test(str);
    },
    isNumber: (str) => {
      const pattern = /^\d+$/;
      return pattern.test(str);
    },
    isSame: (str1, str2) => {
      const deger = str1 === str2;
      return deger;
    },
    isContainsNumber: (str) => {
      const re = /[0-9]/;
      return re.test(str);
    }
  };

  isNullOrUndefined(obj: any) {
    if (obj === undefined || obj === null || obj === '' || obj === ' ') {
      return true;
    } else {
      return false;
    }
  }

  convertFormattedDate(date) {
    if (!this.isNullOrUndefined(date)) {
      if (date.toString().length > 10) {
        return this.datePipe.transform(date, 'dd.MM.yyyy');
      } else {
        return date;
      }
    }
  }

  convertFormattedDatetime(datetime) {
    return this.datePipe.transform(datetime, 'dd.MM.yyyy HH:mm:ss');
  }
}
