import { generateText } from "@tiptap/core";
import type { JSONContent } from "@tiptap/react";
import StarterKit from "@tiptap/starter-kit";
import Link from "@tiptap/extension-link";

export function extractTextFromTiptap(content: JSONContent): string {
  if (!content) return "";
  
  return generateText(content, [
    StarterKit,
    Link.configure({
      openOnClick: false,
      autolink: false,
    }),
  ]);
}
