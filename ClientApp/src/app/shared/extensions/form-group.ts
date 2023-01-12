import { FormGroup } from '@angular/forms';
declare module '@angular/forms' {
  interface FormGroup {
    setDisable(this: FormGroup, disableList: string[]): void;
    setEnable(this: FormGroup, enableList: string[]): void;
    clear(this: FormGroup): void
  }
}

FormGroup.prototype.setDisable = function (this: FormGroup, disableList: string[]): void {
  disableList.forEach(element => {
    this.controls[element.toString()].disable();
  });
};

FormGroup.prototype.setEnable = function (this: FormGroup, enableList: string[]): void {
  enableList.forEach(element => {
    this.controls[element.toString()].enable();
  });
};

FormGroup.prototype.clear = function (this: FormGroup): void {
  // eslint-disable-next-line guard-for-in
  for (const control in this.controls) {
    this.controls[control].setValue('');
  }
};


