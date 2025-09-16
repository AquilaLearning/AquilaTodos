<script lang="ts" setup>
import axios from "axios";
import { useToast } from "primevue";
import { Form } from '@primevue/forms';
import Button from "primevue/button";
import InputText from "primevue/inputtext";
import { ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();
const toast = useToast();

const model = ref({ name: "" });
const saving = ref(false);

const onSubmit = async (): Promise<void> => {

        saving.value = true;

        try {
            await axios.post("/api/todolist", model.value);
            router.push({ name: "Home" });
        } catch (e) {

            toast.add({
                severity: 'error',
                summary: 'Error',
                detail: `${e}`,
                life: 3000
            });
        } finally {
            saving.value = false;
        }
    };
</script>

<template>
    <div>
        <router-link
            to="/"
            class="float-right"
        >
            Back
        </router-link>

        <h2>Create List</h2>

        <Form
            @submit="onSubmit"
        >

        <label for="name-input">Name</label>
        <InputText
            v-model="model.name"
            id="name-input"
            name="name"
        />

        <Button
            id="submit-button"
            type="submit"
            label="Submit"
            :disabled="saving"
        />

        </Form>
    </div>
</template>
