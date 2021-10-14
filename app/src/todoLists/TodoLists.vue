<template>
    <div>
        <b-button
            :to="{ name: 'CreateList' }"
            class="float-right"
        >
            Create
            <b-icon icon="plus" />
        </b-button>

        <h2>My Todo Lists</h2>

        <b-list-group>
            <b-list-group-item
                v-for="list in lists"
                :key="list.id"
                :to="{ name: 'TodoList', params: { listId: list.id } }"
            >
                {{ list.name }}
            </b-list-group-item>
        </b-list-group>
    </div>
</template>

<script lang="ts">
import axios from "axios";
import { Component, Vue } from "vue-property-decorator";

import type { TodoListDto } from "./TodoListDto";

@Component
export default class TodoLists extends Vue {

    public lists: TodoListDto[] = [];

    public async created(): Promise<void> {
        const response = await axios.get<TodoListDto[]>("api/todolist");
        this.lists = response.data;
    }
}
</script>
