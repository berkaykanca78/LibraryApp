import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { BaseComponent } from 'src/app/shared/bases/base.component';

@Component({
  selector: 'app-validator',
  templateUrl: './validator.component.html',
  styleUrls: ['./validator.component.css']
})
export class ValidatorComponent extends BaseComponent implements OnInit  {
  @Input() control = FormControl;

  constructor() {
    super();
  }

  ngOnInit(): void {
  }

  validationControl() {
    if (!this.ch.isNullOrUndefined(this.control)) {
      return this.control['touched'] && this.control['invalid'];
    } else {
      return false;
    }
  }

  getMessage() {
    if (this.control['touched'] && this.control['invalid']) {
      const errorObj = this.control['errors'];
      if (!!errorObj) {
        const hataAdi = Object.keys(errorObj)[0];
        let hataMesaji;
        console.log(errorObj[hataAdi]);
        if (hataAdi === 'required') {
          hataMesaji = 'Bu alan zorunludur.';
        } else if (hataAdi === 'minlength') {
          const requiredLength = errorObj[hataAdi]['requiredLength'];
          hataMesaji = 'En az ' + requiredLength + ' karakter girmelisiniz.';
        } else if (hataAdi === 'maxlength') {
          const requiredLength = errorObj[hataAdi]['requiredLength'];
          hataMesaji = 'En fazla ' + requiredLength + ' karakter girebilirsiniz.';
        } else if (hataAdi === 'min') {
          const min = errorObj[hataAdi]['min'];
          hataMesaji = 'Girilen sayı en az ' + min + ' olabilir.';
        } else if (hataAdi === 'max') {
          const max = errorObj[hataAdi]['max'];
          hataMesaji = 'Girilen sayı en fazla ' + max + ' olabilir.';
        } else if (hataAdi === 'email') {
          hataMesaji = 'Geçersiz bir email girdiniz. (Mail Adresiniz Örneğin username@hotmail.com Gibi Olmalıdır.)';
        } else if (hataAdi === 'regex') {
          hataMesaji = errorObj[hataAdi]['hataMesaji'];
        } else if (hataAdi === 'validUrl') {
          hataMesaji = errorObj[hataAdi]['hataMesaji'];
        } else if (hataAdi === 'pattern') {
          if (errorObj[hataAdi]['requiredPattern'] == this.globals.phoneRegex) {
            hataMesaji = 'Telefon Formatınız Uygun Değildir. (Telefon Numaranız Örneğin 05328451675 Gibi Olmalıdır.)';
          } else if (errorObj[hataAdi]['requiredPattern'] == this.globals.passwordRegex) {
            hataMesaji = 'Şifre Formatınız Uygun Değildir. (Şifreniz En Az 1 Sayı, 1 Büyük Harf, 1 Küçük Harf, 1 Sayısal Olmayan İfade ve 15 Karakter Olmalıdır)';
          } else {
            hataMesaji = 'Formatınız Uygun Değildir.';
          }
        }
        return hataMesaji;
      }
    } else {
      return '';
    }
  }
  validationControlText(text) {
    if (this.ch.isNullOrUndefined(text)) {
      return false;
    }
    return true;
  }
}
