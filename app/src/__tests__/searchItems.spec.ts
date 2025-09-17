import { searchItems } from "@/todoLists/items/searchItems";
import type { TodoItemDto } from "@/todoLists/items/TodoItemDto";
import { describe, expect, it } from "vitest";

const items:TodoItemDto[] = [
    {
        id: 1,
        label: "Buy eggs",
        complete: false
    },
    {
        id: 2,
        label: "Do laundry",
        complete: false
    },
    {
        id: 3,
        label: "Go for a run",
        complete: false
    }
];

describe("searchItems", () => {

    it("should...", () => {

        const search = "";
        const result = searchItems(items, search);
        expect(result).toBeTruthy();
    });
});