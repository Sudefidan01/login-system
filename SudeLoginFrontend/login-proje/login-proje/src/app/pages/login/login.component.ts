import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  KullaniciName: string = '';
  KullaniciPassword: string = '';
  errorMessage: string = '';
  animateLogo:boolean=false;

  constructor(private http: HttpClient, private router: Router) {}

  async login() {
    const loginData = {
      kullaniciName: this.KullaniciName,
      kullaniciPassword: this.KullaniciPassword
    };

    try {
      const response: any = await firstValueFrom(
        this.http.post('http://localhost:5062/api/kullanici/login', loginData,{
          withCredentials:true
        })
      );

      console.log(response);

      
      if (response && response.kullanici) {
        this.errorMessage = '';
        this.animateLogo=true;
        setTimeout(()=>{
          this.router.navigate(['/dashboard'], {
          state: { kullanici: response.kullanici }
        });
        },1500);

      } else {
        this.errorMessage = 'Sunucudan geçersiz yanıt alındı.';
      }

    } catch (error: any) {
      console.error('Giriş Başarısız:', error);

      if (error.status === 401 && error.error?.mesaj) {
        this.errorMessage = error.error.mesaj;
      } else if (error.status === 0) {
        this.errorMessage = 'Sunucuya ulaşılamıyor. API çalışıyor mu kontrol et.';
      } else {
        this.errorMessage = 'Bir hata oluştu. Lütfen tekrar deneyin.';
      }
    }
  }
}