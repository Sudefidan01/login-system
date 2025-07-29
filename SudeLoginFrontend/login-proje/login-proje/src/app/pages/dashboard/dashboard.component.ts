import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  kullanici: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('http://localhost:5062/api/kullanici/me', {
      withCredentials: true
    }).subscribe({
      next: (data) => {
        this.kullanici = data;
        console.log('Kullanıcı bilgisi:', this.kullanici);
      },
      error: (err) => {
        console.error('Kullanıcı bilgisi alınamadı:', err);
      }
    });
  }


  getCookie(name: string): string | null {
    const cookies = document.cookie.split(';');
    for (let cookie of cookies) {
      const [key, value] = cookie.trim().split('=');
      if (key === name) {
        return decodeURIComponent(value);
      }
    }
    return null;
  }
}