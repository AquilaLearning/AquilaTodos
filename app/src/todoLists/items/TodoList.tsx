import axios from "axios";
import type { VNode } from "vue";
import { Component, Prop, Vue } from "vue-property-decorator";
import { BButton, BForm, BFormCheckbox, BFormInput, BListGroup, BListGroupItem } from "bootstrap-vue";

import type { TodoListDto } from "../TodoListDto";

import type { TodoItemDto } from "./TodoItemDto";

@Component
export class TodoList extends Vue {

    @Prop({ type: [String, Number], required: true })
    public listId!: string | number;

    private list: TodoListDto | null = null;

    private items: TodoItemDto[] = [];

    private newItem: string = "";
    private saving: boolean = false;

    public created(): void {
        this.loadList();
        this.loadItems();
    }

    public render(): VNode {
        return (
            <div>
                <router-link
                    to="/"
                    class="float-right"
                >
                    Back
                </router-link>
                <h2>{this.list?.name}</h2>

                <BForm
                    class="d-flex align-items-center mb-3 mt-4"
                    onSubmit={this.onAddItemSubmit}
                >
                    <BFormInput
                        v-model={this.newItem}
                        placeholder="New todo item..."
                        class="flex-grow-1 mr-3 w-auto"
                    />
                    <BButton type="submit">
        Add
                        <b-icon icon="plus" />
                    </BButton>
                </BForm>

                <BListGroup>
                    {this.items.map(item => (
                        <BListGroupItem
                            class="d-flex justify-content-between"
                        >
                            {item.label}

                            <BFormCheckbox
                                checked={item.complete}
                            />

                        </BListGroupItem>
                    ))}
                </BListGroup>
            </div>
        );
    }

    private async loadList(): Promise<void> {
        const listResponse = await axios.get<TodoListDto>(`/api/todolist/${this.listId}`);
        this.list = listResponse.data;
    }

    private async loadItems(): Promise<void> {
        const response = await axios.get<TodoItemDto[]>(`/api/todolist/${this.listId}/item`);
        this.items = response.data;
    }

    private async onAddItemSubmit(event: Event): Promise<void> {
        event.preventDefault();

        this.saving = true;

        try {
            await axios.post(`/api/todolist/${this.listId}/item`, { label: this.newItem });
            this.newItem = "";
            await this.loadItems();
        } finally {
            this.saving = false;
        }

    }
}
