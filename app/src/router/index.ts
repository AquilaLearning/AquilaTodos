import Vue from "vue";
import type { RouteConfig } from "vue-router";
import VueRouter from "vue-router";

import { todoListRoutes } from "../todoLists";

Vue.use(VueRouter);

const routes: RouteConfig[] = [
    ...todoListRoutes
];

const router = new VueRouter({
    mode: "history",
    base: process.env.BASE_URL,
    routes
});

export default router;
