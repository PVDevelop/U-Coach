import {Component} from 'angular2/core';
import {NgForm} from 'angular2/common';
import {User} from '../../models/user';

@Component({
    selector: 'user-form',
    templateUrl: 'app/components/user-form/template.html'
})

export class UserFormComponent{
    user = new User("", "");
    
    onSubmit(){
        alert("Submitted!");
    }
}