// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.addEventListener('DOMContentLoaded', e => {
  const theme = localStorage.getItem('theme');
  const html = document.querySelector('html');
  const lightLink = document.querySelector('#theme-light-link');
  const darkLink = document.querySelector('#theme-dark-link');
  
  function setLight() {
    html.setAttribute('data-bs-theme', 'light');
    localStorage.setItem('theme', 'light');
    darkLink.classList.remove('d-none');
    lightLink.classList.add('d-none');
  }
  
  function setDark() {
    html.setAttribute('data-bs-theme', 'dark');
    lightLink.classList.remove('d-none');
    darkLink.classList.add('d-none');
    localStorage.setItem('theme', 'dark');
  }
  
  if (theme === 'dark') {
    setDark();
  } else {
    setLight();
  }
  lightLink.addEventListener('click', e => {
    e.preventDefault();
    setLight();
  });
  darkLink.addEventListener('click', e => {
    e.preventDefault();
    setDark();
  });
});