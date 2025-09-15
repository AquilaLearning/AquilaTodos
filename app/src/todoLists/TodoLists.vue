<script lang="ts" setup>
import { onMounted, ref } from "vue";
import axios from "axios";
import Button from "primevue/button";
import type { TodoListDto } from "./TodoListDto";

const lists = ref<TodoListDto[]>([]);

onMounted(async () => {
    const response = await axios.get<TodoListDto[]>("api/todolist");
    lists.value = response.data;
});

</script>
<template>
    <div>
        <Button
            :to="{ name: 'CreateList' }"
            class="float-right"
        >
            Create
            <b-icon icon="plus" />
        </Button>

        <h2>My Todo Lists</h2>

        <ul>
            <li
                v-for="list in lists"
                :key="list.id"
                class="list-group-item"
            >
                <router-link :to="{ name: 'TodoList', params: { listId: list.id } }">
                    {{ list.name }}
                </router-link>
        </li>
        </ul>
    </div>
</template>
