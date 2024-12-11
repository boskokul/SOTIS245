import { Component, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-tree-node',
  templateUrl: './tree-node.component.html',
  styleUrls: ['./tree-node.component.css'],
})
export class TreeNodeComponent {
  @Input() node: any;
  @Output() termClick = new EventEmitter<string>();

  getColor(isConcept?: boolean): string {
    return isConcept ? 'green' : 'blue';
  }

  onTermClick(term: string) {
    this.termClick.emit(term);
  }
}
