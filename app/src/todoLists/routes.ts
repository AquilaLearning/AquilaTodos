import type { RouteRecordRaw } from "vue-router";

import CreateList from "./CreateList.vue";
import TodoLists from "./TodoLists.vue";
import TodoList from "./items/TodoList.vue";

export const todoListRoutes: RouteRecordRaw[] = [
    {
        path: "/",
        name: "Home",
        component: TodoLists
    },
    {
        path: "/list/create",
        name: "CreateList",
        component: CreateList
    },
    {
        path: "/list/:listId",
        name: "TodoList",
        component: TodoList,
        props: true
    }
];
