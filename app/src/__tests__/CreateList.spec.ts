import { flushPromises, mount } from "@vue/test-utils";
import type { AxiosStatic } from "axios";
import axios from "axios";
import { describe, expect, it, vi, type Mocked } from "vitest";

import CreateList from "@/todoLists/CreateList.vue";

vi.mock("axios");
const mockedAxios = axios as Mocked<AxiosStatic>;

describe("CreateList.vue", () => {
    it("should render create list form", () => {
        const wrapper = mount(CreateList);

        const header = wrapper.find("h2");
        expect(header.text()).toBe("Create List");

        const input = wrapper.find("#name-input");
        expect(input).toBeDefined();

        const button = wrapper.find("#submit-button");
        expect(button.text()).toBe("Submit");
    });

    it("should submit new list to api", async () => {
        const wrapper = mount(
            CreateList,
            {
                attachTo: document.body
            });

        const input = wrapper.find("#name-input");
        (input.element as HTMLInputElement).value = "Test List";
        await input.trigger("input");

        const button = wrapper.find("#submit-button");
        await button.trigger("click");

        await flushPromises();

        expect(mockedAxios.post).toBeCalledWith(
            "/api/todolist",
            { name: "Test List" }
        );
    });
});