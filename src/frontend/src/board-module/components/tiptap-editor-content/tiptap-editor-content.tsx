import React from "react";
import {useEditor, EditorContent, type JSONContent} from "@tiptap/react";
import StarterKit from "@tiptap/starter-kit";

import "./tiptap-editor-content.css";

type TipTapEditorContentProps = {
  content: JSONContent;
  editable?: boolean;
  className?: string;
};

export const TipTapEditorContent: React.FC<TipTapEditorContentProps> = ({
  content,
  editable = false,
  className,
}) => {
  const editor = useEditor({
    extensions: [StarterKit],

    content,
    editable,
  });

  return <EditorContent editor={editor} className={className} />;
};
