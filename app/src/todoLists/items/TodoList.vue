<script lang="ts" setup>
import { computed, onMounted, ref } from "vue";
import axios from "axios";
import { Form, type FormSubmitEvent } from '@primevue/forms';
import Button from "primevue/button";
import Checkbox from 'primevue/checkbox';
import InputText from "primevue/inputtext";

import type { TodoListDto } from "../TodoListDto";

import { searchItems } from "./searchItems";
import type { TodoItemDto } from "./TodoItemDto";

interface TodoListProps {
    listId: string;
}

const props = defineProps<TodoListProps>();

const list = ref<TodoListDto | null>(null);
const items = ref<TodoItemDto[]>([]);

const newItem = ref("");
const search = ref("");
const saving = ref(false);

const filteredItems = computed(() => searchItems(items.value, search.value))

const loadList = async (): Promise<void> => {
    const listResponse = await axios.get<TodoListDto>(`/api/todolist/${props.listId}`);
    list.value = listResponse.data;
};

const loadItems = async(): Promise<void> => {
    const response = await axios.get<TodoItemDto[]>(`/api/todolist/${props.listId}/item`);
    items.value = response.data;
}

const onAddItemSubmit = async (event: FormSubmitEvent): Promise<void> => {
    event.originalEvent.preventDefault();

    saving.value = true;

    try {
        await axios.post(`/api/todolist/${props.listId}/item`, { label: newItem.value });
        newItem.value = "";
        await loadItems();
    } finally {
        saving.value = false;
    }
};

onMounted(() => {
    loadList();
    loadItems();
});

</script>
<template>
    <div>
        <router-link
            to="/"
            class="float-right"
        >
            Back
        </router-link>
        <h2>{{ list?.name }}</h2>

        <Form
            @submit="onAddItemSubmit"
        >
            <InputText
                v-model="newItem"
                name="item"
                placeholder="New todo item..."
            />
            <Button type="submit" label="Add" />
        </Form>

        <ul>
            <li
                v-for="item in filteredItems"
                :key="item.id"
                class="list-group-item"
            >
                <Checkbox v-model="item.complete" />
                {{ item.label }}
            </li>
        </ul>
    </div>
</template>
