import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";

// Bootstrap import
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

import "@/scss/custom.scss";

createApp(App).use(store).use(router).mount("#app");
