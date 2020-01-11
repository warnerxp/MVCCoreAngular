import { Component, OnInit } from '@angular/core';

import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { User } from 'src/app/_models/user';




@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
users: User[];

  constructor(private userService: UserService, private alertify: AlertifyService ) { }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {

    this.userService.getUsers()
    .subscribe((res: any) => {
      this.users = res;
    }, error => {
      this.alertify.error(error);
    });




  }

}
