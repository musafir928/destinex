import { Component, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Role, User } from '../../types/user';

@Component({
  selector: 'app-admin',
  standalone: true,
  templateUrl: './admin.html',
})
export class Admin {
  private http = inject(HttpClient);
  private baseUrl = '';

  users = signal<User[]>([]);
  loading = signal(false);

  readonly roles = ['Admin', 'User'];

  constructor() {
    this.loadUsers();
  }

  loadUsers() {
    this.loading.set(true);
    this.http.get<User[]>(`${this.baseUrl}/api/admin/users`).subscribe({
      next: (res) => {
        this.users.set(res);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  updateRole(userId: string, role: Role) {
    this.http.post<User>(`${this.baseUrl}/api/admin/update/role`, { role, id: userId }).subscribe({
      next: (updated) => {
        this.users.update((list) =>
          list.map((u) => (u.id === userId ? { ...u, role: updated.role } : u))
        );
      },
    });
  }

  deleteUser(userId: string) {
    this.http.delete(`${this.baseUrl}/api/admin/${userId}`).subscribe({
      next: () => {
        this.users.update((list) => list.filter((u) => u.id !== userId));
      },
    });
  }
}
