import { beforeEach, describe, expect, it, vi, type Mocked } from "vitest";
import { flushPromises, mount } from "@vue/test-utils";
import type { AxiosStatic } from "axios";
import axios from "axios";

import TodoLists from "@/todoLists/TodoLists.vue";
import type { TodoListDto } from "@/todoLists/TodoListDto";

vi.mock("axios");
const mockedAxios = axios as Mocked<AxiosStatic>;

describe("TodoLists.vue", () => {

    beforeEach(() => {

        const lists: TodoListDto[] = [
            {
                id: 1,
                name: "List 1"
            },
            {
                id: 2,
                name: "List 2"
            }
        ];

        mockedAxios.get.mockResolvedValue({ data: lists });
    });

    it("should render todo lists returned from the API", async () => {

        const wrapper = mount(TodoLists);
        await flushPromises();

        const header = wrapper.find("h2");
        expect(header.text()).toBe("My Todo Lists");

        const lists = wrapper.findAll(".list-group-item");
        expect(lists.length).toBe(2);
        expect(lists.at(0)?.text()).toBe("List 1");
        expect(lists.at(1)?.text()).toBe("List 2");
    });
});