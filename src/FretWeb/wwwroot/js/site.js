// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.addEventListener('DOMContentLoaded', e => {
  const theme = localStorage.getItem('theme');
  const lightLink = document.querySelector('#theme-light-link');
  const darkLink = document.querySelector('#theme-dark-link');
  const themeStylesheet = document.querySelector('#theme-stylesheet');
  const fretboardStylesheet = document.querySelector('#fretboard-stylesheet');
  const mainNav = document.querySelector('#main-nav');
  
  function setLight() {
    themeStylesheet.href = '/lib/bootswatch/flatly.min.css';
    fretboardStylesheet.href = '/css/fretboard-light.css';
    mainNav.classList.remove('navbar-dark');
    mainNav.classList.add('navbar-light');
    localStorage.setItem('theme', 'light');
    darkLink.classList.remove('d-none');
    lightLink.classList.add('d-none');
  }
  
  function setDark() {
    themeStylesheet.href = '/lib/bootswatch/darkly.min.css';
    fretboardStylesheet.href = '/css/fretboard-dark.css';
    mainNav.classList.remove('navbar-light');
    mainNav.classList.add('navbar-dark');
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