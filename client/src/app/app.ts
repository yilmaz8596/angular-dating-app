import { Component, inject, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { Nav } from '../layout/nav/nav';
import { Home } from '../features/home/home';
import { Register } from '../features/account/register/register';
import { AccountService } from '../core/services/account-service';
import { User } from '../types/user';

@Component({
  selector: 'app-root',
  imports: [Nav, Home, Register],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  protected title = 'Dating app';
  private http = inject(HttpClient);
  protected members = signal<User[]>([]);

  async ngOnInit() {
    this.members.set(await this.getMembers());
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  async getMembers() {
    try {
      return await lastValueFrom(
        this.http.get<User[]>('https://localhost:5001/api/members')
      );
    } catch (error) {
      console.error('Error fetching members:', error);
      throw error;
    }
  }
}
