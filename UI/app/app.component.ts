import {Component} from 'angular2/core';
import {UserFormComponent} from './components/user-form/component'

@Component({
    selector: 'app-main',
    template: '<user-form></user-form>',
    directives: [UserFormComponent]
})
export class AppComponent { }