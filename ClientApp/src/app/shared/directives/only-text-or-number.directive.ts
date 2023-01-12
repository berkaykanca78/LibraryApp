/* eslint-disable @angular-eslint/directive-selector */
import { Directive, ElementRef, HostListener } from '@angular/core';


@Directive({
    selector: 'input[textOrNumbersOnly]'
})
export class TextOrNumberDirective {

    private navigationKeys = [
        'Backspace',
        'Delete',
        'Tab',
        'Escape',
        'Enter',
        'Home',
        'End',
        'ArrowLeft',
        'ArrowRight',
        'Clear',
        'Copy',
        'Paste',
    ];
    constructor(private _el: ElementRef) { }

    @HostListener('keydown', ['$event'])
    onKeyDown(e: KeyboardEvent) {
        if (
            // Allow: Delete, Backspace, Tab, Escape, Enter, etc
            this.navigationKeys.indexOf(e.key) > -1
        ) {
            return;
        }
        if (
            (e.key !== 'a') &&
            (e.key !== 'b') &&
            (e.key !== 'c') &&
            (e.key !== 'd') &&
            (e.key !== 'e') &&
            (e.key !== 'f') &&
            (e.key !== 'g') &&
            (e.key !== 'h') &&
            (e.key !== 'i') &&
            (e.key !== 'j') &&
            (e.key !== 'k') &&
            (e.key !== 'l') &&
            (e.key !== 'm') &&
            (e.key !== 'n') &&
            (e.key !== 'o') &&
            (e.key !== 'p') &&
            (e.key !== 'q') &&
            (e.key !== 'r') &&
            (e.key !== 's') &&
            (e.key !== 't') &&
            (e.key !== 'u') &&
            (e.key !== 'v') &&
            (e.key !== 'y') &&
            (e.key !== 'z') &&
            (e.key !== 'ç') &&
            (e.key !== 'ğ') &&
            (e.key !== 'ı') &&
            (e.key !== 'ö') &&
            (e.key !== 'ş') &&
            (e.key !== 'ü') &&
            (e.key !== 'x') &&
            (e.key !== 'w') &&
            (e.key !== 'q') &&
            (e.key !== 'A') &&
            (e.key !== 'B') &&
            (e.key !== 'C') &&
            (e.key !== 'D') &&
            (e.key !== 'E') &&
            (e.key !== 'F') &&
            (e.key !== 'G') &&
            (e.key !== 'H') &&
            (e.key !== 'İ') &&
            (e.key !== 'J') &&
            (e.key !== 'K') &&
            (e.key !== 'L') &&
            (e.key !== 'M') &&
            (e.key !== 'N') &&
            (e.key !== 'O') &&
            (e.key !== 'P') &&
            (e.key !== 'Q') &&
            (e.key !== 'R') &&
            (e.key !== 'S') &&
            (e.key !== 'T') &&
            (e.key !== 'U') &&
            (e.key !== 'V') &&
            (e.key !== 'Y') &&
            (e.key !== 'Z') &&
            (e.key !== 'Ç') &&
            (e.key !== 'Ğ') &&
            (e.key !== 'I') &&
            (e.key !== 'Ö') &&
            (e.key !== 'Ş') &&
            (e.key !== 'Ü') &&
            (e.key !== 'X') &&
            (e.key !== 'W') &&
            (e.key !== 'Q') &&
            (e.key !== '0') &&
            (e.key !== '1') &&
            (e.key !== '2') &&
            (e.key !== '3') &&
            (e.key !== '4') &&
            (e.key !== '5') &&
            (e.key !== '6') &&
            (e.key !== '7') &&
            (e.key !== '8') &&
            (e.key !== '9')
        ) {
            e.preventDefault();
        }
    }

}
