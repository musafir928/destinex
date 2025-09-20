import { Injectable } from '@angular/core';

const TOAST_CONTAINER_ID = 'toast-container';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  constructor() {
    this.createToastContainer();
  }

  private createToastContainer() {
    if (!document.getElementById(TOAST_CONTAINER_ID)) {
      const container = document.createElement('div');
      container.id = TOAST_CONTAINER_ID;
      container.className = 'toast toast-bottom toast-end';
      document.body.appendChild(container);
    }
  }

  private createToastElement(message: string, alertClass: string, duration = 5000) {
    const toastContainer = document.getElementById(TOAST_CONTAINER_ID);
    if (!toastContainer) return;

    const toast = document.createElement('div');
    toast.classList.add('alert', alertClass, 'shadow-lg');
    toast.innerHTML = `
    <span>${message}</span>
    <button class="ml-4 btn btn-sm btn-ghost">X</button>
    `;

    toast.querySelector('button')?.addEventListener('click', () => {
      toastContainer.removeChild(toast);
    });

    toastContainer.append(toast);

    setTimeout(() => {
      if (toastContainer.contains(toast)) {
        toastContainer.removeChild(toast);
      }
    }, duration);
  }

  success(message: string, duration?: number) {
    this.createToastElement(message, 'alert-success', duration);
  }

  error(message: string, duration?: number) {
    this.createToastElement(message, 'alert-error', duration);
  }

  warning(message: string, duration?: number) {
    this.createToastElement(message, 'alert-warning', duration);
  }

  info(message: string, duration?: number) {
    this.createToastElement(message, 'alert-info', duration);
  }
}
