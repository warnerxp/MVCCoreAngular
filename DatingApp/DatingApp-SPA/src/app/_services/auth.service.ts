import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'https://localhost:44320/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

constructor(private http: HttpClient) {}

login(model: any) {
  console.log('Enter Service');
  return this.http.post(this.baseUrl + 'login', model)
.pipe(
  map((response: any) => {
    const user = response;
    if (user) {
      localStorage.setItem('token', user.token);
      this.decodedToken = this.jwtHelper.decodeToken(user.token);
      console.log(this.decodedToken);
    }
  } )
);
}
register(model: any) {
  return this.http.post(this.baseUrl + 'register', model);
}

loggedIn() {

  const  token = localStorage.getItem('token');
  if (token == null ) {
    console.log("Es nulo el token");
    return false;
  } else {
console.log('El token no es nulo ');
    return !this.jwtHelper.isTokenExpired(localStorage.getItem('token'));
  }


}

}
