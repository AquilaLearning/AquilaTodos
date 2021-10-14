import { mount } from "@vue/test-utils";
import type { AxiosStatic } from "axios";
import axios from "axios";
import flushPromises from "flush-promises";

import { TodoList } from "@/todoLists/items/TodoList";
import type { TodoListDto } from "@/todoLists/TodoListDto";
import type { TodoItemDto } from "@/todoLists/items/TodoItemDto";

jest.mock("axios");
const mockedAxios = axios as jest.Mocked<AxiosStatic>;

describe("TodoLists.vue", () => {

    beforeEach(() => {

        const list: TodoListDto = {
            id: 1,
            name: "List 1"
        };

        const items: TodoItemDto[] = [
            {
                id: 1,
                label: "Item 1",
                complete: false
            },
            {
                id: 2,
                label: "Item 2",
                complete: false
            },
            {
                id: 3,
                label: "Item 3",
                complete: false
            }
        ];

        mockedAxios.get.mockImplementation(url => {
            switch (url) {
                case "/api/todolist/1":
                    return Promise.resolve({ data: list });
                case "/api/todolist/1/item":
                    return Promise.resolve({ data: items });
                default:
                    return Promise.reject(new Error("not found"));
            }
        });
    });

    it("should render todo lists returned from the API", async () => {

        const wrapper = mount(
            TodoList,
            {
                propsData: {
                    listId: 1
                }
            });

        await flushPromises();

        const header = wrapper.find("h2");
        expect(header.text()).toBe("List 1");

        const lists = wrapper.findAll(".list-group-item");
        expect(lists.length).toBe(3);
        expect(lists.at(0).text()).toBe("Item 1");
        expect(lists.at(1).text()).toBe("Item 2");
        expect(lists.at(2).text()).toBe("Item 3");
    });
});
