import { createRouter, createWebHistory } from 'vue-router'

import { todoListRoutes } from "../todoLists";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    ...todoListRoutes
  ],
})

export default router
