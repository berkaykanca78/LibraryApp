import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'uppercaseTurkish'
})

export class UppercaseturkishPipe implements PipeTransform {
    transform(value: string) {
        if (value === null || value === undefined || value == '' ) {
            return value;
        }
        return value.toLocaleUpperCase();
    }
}
