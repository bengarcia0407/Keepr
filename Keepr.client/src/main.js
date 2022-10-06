import '@mdi/font/css/materialdesignicons.css'
import 'bootstrap'
import { createApp } from 'vue'
// @ts-ignore
import App from './App.vue'
import { registerGlobalComponents } from './registerGlobalComponents'
import { router } from './router'

// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyBU6XoizmM2eMtQgyxOQgIykCaYom7ssC4",
  authDomain: "keepr-75c5f.firebaseapp.com",
  projectId: "keepr-75c5f",
  storageBucket: "keepr-75c5f.appspot.com",
  messagingSenderId: "161120860291",
  appId: "1:161120860291:web:87c975c9403a3cbaf9c3b6",
  measurementId: "G-LRZGRMDT2G"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);

const root = createApp(App)
registerGlobalComponents(root)

root
  .use(router)
  .mount('#app')
