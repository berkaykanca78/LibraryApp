import { Component } from '@angular/core';
import { GlobalVariables } from '../constants/global-variables';
import { CommonHelper } from '../helpers/common-helper';
import { AppInjector } from './app-injector.service';

@Component({
    template: '',
})
export class BaseComponent {
    public ch: CommonHelper;
    public globals: GlobalVariables;

    constructor() {
      const injector = AppInjector.getInjector();
        this.ch = injector.get(CommonHelper);
        this.globals = injector.get(GlobalVariables);
    }
}
