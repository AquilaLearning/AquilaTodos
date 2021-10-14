import type { RouteConfig } from "vue-router";

import CreateList from "./CreateList.vue";
import TodoLists from "./TodoLists.vue";
import { TodoList } from "./items/TodoList";

export const todoListRoutes: RouteConfig[] = [
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
