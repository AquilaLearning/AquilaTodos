<template>
    <div>
        <router-link
            to="/"
            class="float-right"
        >
            Back
        </router-link>

        <h2>Create List</h2>

        <b-form
            class="text-left"
            @submit.prevent="onSubmit"
        >
            <b-form-group
                label="Name"
                label-for="name-input"
            >
                <b-form-input
                    id="name-input"
                    v-model="model.name"
                    :disabled="saving"
                />
            </b-form-group>

            <div class="text-right">
                <b-button
                    id="submit-button"
                    type="submit"
                    variant="primary"
                    :disabled="saving"
                >
                    Submit
                </b-button>
            </div>
        </b-form>
    </div>
</template>

<script lang="ts">
import axios from "axios";
import { Component, Vue } from "vue-property-decorator";

@Component
export default class CreateList extends Vue {

    public model = {
        name: ""
    };

    public saving = false;

    public async onSubmit(): Promise<void> {

        this.saving = true;

        try {
            await axios.post("/api/todolist", this.model);
            this.$router.push({ name: "Home" });
        } catch (e) {
            this.$bvToast.toast(
                `${e}`,
                {
                    title: "Error!",
                    variant: "danger"
                }
            );
        } finally {
            this.saving = false;
        }
    }
}
</script>
