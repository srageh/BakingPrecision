const API_BASE_URL =
  process.env.NEXT_PUBLIC_API_URL || "https://localhost:7115/api";

export interface Recipe {
  id: number;
  title: string;
  category: string;
  details: string;
}

export type NewRecipe = Omit<Recipe, "id">;
export const api = {
  getAllRecipes: async (): Promise<Recipe[]> => {
    const response = await fetch(`${API_BASE_URL}/Recipes`, {
      cache: "no-store",
    });
    if (!response.ok) {
      throw new Error("Failed to fetch recipes");
    }
    return response.json();
  },

  createRecipe: async (newRecipe: NewRecipe): Promise<Recipe> => {
    const response = await fetch(`${API_BASE_URL}/Recipes`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newRecipe),
    });

    if (!response.ok) {
      throw new Error("Failed to create new recipe");
    }
    return response.json();
  },

  extractFromImage: async (file: File): Promise<Recipe> => {
    const formData = new FormData();
    formData.append("image", file);
    const response = await fetch(
      `${API_BASE_URL}/recipe-extraction/from-image`,
      {
        method: "POST",
        body: formData,
      },
    );

    if (!response.ok) {
      throw new Error("Failed to upload image");
    }
    return response.json();
  },
};
